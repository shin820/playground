import { Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { RoleServiceProxy, RoleListDto } from '@shared/service-proxies/service-proxies';

@Component({
  // selector: 'app-roles',
  templateUrl: './roles.component.html'
})
export class RolesComponent extends AppComponentBase implements OnInit {

  roles: RoleListDto[] = [];

  constructor(
     injector: Injector,
     private _roleService: RoleServiceProxy
  ) { 
    super(injector);
  }

  ngOnInit() {
    this.getRoles();
  }

  getRoles():void {
    this._roleService.getRoles().subscribe((result)=>{this.roles=result.items});
  }

}
