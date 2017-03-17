import { LuxTemplatePage } from './app.po';

describe('abp-project-name-template App', function() {
  let page: LuxTemplatePage;

  beforeEach(() => {
    page = new LuxTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
