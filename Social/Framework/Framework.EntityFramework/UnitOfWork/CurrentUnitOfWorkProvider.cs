﻿using Castle.Core;
using System;
using System.Collections.Concurrent;
using System.Runtime.Remoting.Messaging;

namespace Framework.EntityFramework.UnitOfWork
{
    public class CurrentUnitOfWorkProvider : ICurrentUnitOfWorkProvider
    {
        [DoNotWire]
        public IUnitOfWork Current
        {
            get { return GetCurrentUow(); }
            set { SetCurrentUow(value); }
        }

        private const string ContextKey = "UnitOfWork.Current";

        private static readonly ConcurrentDictionary<string, IUnitOfWork> UnitOfWorkDictionary = new ConcurrentDictionary<string, IUnitOfWork>();

        private static IUnitOfWork GetCurrentUow()
        {
            var unitOfWorkKey = CallContext.LogicalGetData(ContextKey) as string;
            if (unitOfWorkKey == null)
            {
                return null;
            }

            IUnitOfWork unitOfWork;
            if (!UnitOfWorkDictionary.TryGetValue(unitOfWorkKey, out unitOfWork))
            {
                //logger.Warn("There is a unitOfWorkKey in CallContext but not in UnitOfWorkDictionary (on GetCurrentUow)! UnitOfWork key: " + unitOfWorkKey);
                CallContext.FreeNamedDataSlot(ContextKey);
                return null;
            }

            if (unitOfWork.IsDisposed)
            {
                //logger.Warn("There is a unitOfWorkKey in CallContext but the UOW was disposed!");
                UnitOfWorkDictionary.TryRemove(unitOfWorkKey, out unitOfWork);
                CallContext.FreeNamedDataSlot(ContextKey);
                return null;
            }

            return unitOfWork;
        }

        private static void SetCurrentUow(IUnitOfWork value)
        {
            if (value == null)
            {
                ExitFromCurrentUowScope();
                return;
            }

            var unitOfWorkKey = CallContext.LogicalGetData(ContextKey) as string;
            if (unitOfWorkKey != null)
            {
                IUnitOfWork outer;
                if (UnitOfWorkDictionary.TryGetValue(unitOfWorkKey, out outer))
                {
                    if (outer == value)
                    {
                       //logger.Warn("Setting the same UOW to the CallContext, no need to set again!");
                        return;
                    }

                    value.Outer = outer;
                }
                else
                {
                    //logger.Warn("There is a unitOfWorkKey in CallContext but not in UnitOfWorkDictionary (on SetCurrentUow)! UnitOfWork key: " + unitOfWorkKey);
                }
            }

            unitOfWorkKey = value.Id;
            if (!UnitOfWorkDictionary.TryAdd(unitOfWorkKey, value))
            {
                throw new InvalidOperationException("Can not set unit of work! UnitOfWorkDictionary.TryAdd returns false!");
            }

            CallContext.LogicalSetData(ContextKey, unitOfWorkKey);
        }

        private static void ExitFromCurrentUowScope()
        {
            var unitOfWorkKey = CallContext.LogicalGetData(ContextKey) as string;
            if (unitOfWorkKey == null)
            {
                //logger.Warn("There is no current UOW to exit!");
                return;
            }

            IUnitOfWork unitOfWork;
            if (!UnitOfWorkDictionary.TryGetValue(unitOfWorkKey, out unitOfWork))
            {
                //logger.Warn("There is a unitOfWorkKey in CallContext but not in UnitOfWorkDictionary (on ExitFromCurrentUowScope)! UnitOfWork key: " + unitOfWorkKey);
                CallContext.FreeNamedDataSlot(ContextKey);
                return;
            }

            UnitOfWorkDictionary.TryRemove(unitOfWorkKey, out unitOfWork);
            if (unitOfWork.Outer == null)
            {
                CallContext.FreeNamedDataSlot(ContextKey);
                return;
            }

            //Restore outer UOW

            var outerUnitOfWorkKey = unitOfWork.Outer.Id;
            if (!UnitOfWorkDictionary.TryGetValue(outerUnitOfWorkKey, out unitOfWork))
            {
                //No outer UOW
                //logger.Warn("Outer UOW key could not found in UnitOfWorkDictionary!");
                CallContext.FreeNamedDataSlot(ContextKey);
                return;
            }

            CallContext.LogicalSetData(ContextKey, outerUnitOfWorkKey);
        }
    }
}
