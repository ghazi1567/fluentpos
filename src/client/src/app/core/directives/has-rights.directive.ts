import { Directive, Input, OnInit, TemplateRef, ViewContainerRef } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Directive({
  selector: '[appHasRights]'
})
export class HasRightsDirective implements OnInit{
  @Input() appHasRights: any;
  @Input() prefix: string = 'Permissions';

  constructor(private viewContainerRef: ViewContainerRef, 
    private templateRef: TemplateRef<any>, 
    private authService: AuthService) {}

    ngOnInit(): void {
      let permission = `${this.prefix}.${this.appHasRights.module}.${this.appHasRights.action}`;
      const isAuthorized = this.authService.isAuthorized('Permission', [permission]);
      if (!isAuthorized) {
        this.viewContainerRef.clear();
      } else {
        this.viewContainerRef.createEmbeddedView(this.templateRef);
      }
    }

}
