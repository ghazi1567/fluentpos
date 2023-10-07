import { Component, Inject, OnInit } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/core/services/auth.service';
import { filter } from 'rxjs/operators';
import { NB_AUTH_OPTIONS, NbAuthSocialLink } from '@nebular/auth';
import { getDeepFromObject } from 'src/app/core/helpers/clone-helper';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  redirectDelay: number = 0;
  showMessages: any = {};
  strategy: string = '';
  options: any = {}
  errors: string[] = [];
  messages: string[] = [];
  user: any = {};
  submitted: boolean = false;
  socialLinks: NbAuthSocialLink[] = [];
  rememberMe = false;

  loginForm: UntypedFormGroup;
  returnUrl: string;
  isBeingLoggedIn: boolean = false;
  constructor(private authService: AuthService,
    private router: Router,
    private activatedRoute: ActivatedRoute) {
    this.initializeForm();
  }

  ngOnInit(): void {
    this.returnUrl = this.activatedRoute.snapshot.queryParams.returnUrl || '/home';
  }

  initializeForm() {
    this.loginForm = new UntypedFormGroup({
      email: new UntypedFormControl('', Validators.required && Validators.email),
      password: new UntypedFormControl('', Validators.required)
    });
  }
  login() { 
    console.log(this.user);
    this.authService.login(this.user)
      .pipe(filter(result => result?.succeeded === true))
      .subscribe(() => this.router.navigateByUrl(this.returnUrl),
        error => { console.log(error); this.loginForm.enable(); }).add(() => this.isBeingLoggedIn = false);
  }
  onSubmit() {
    this.isBeingLoggedIn = true;
    this.loginForm.disable()
    console.log(this.loginForm.value);
    this.authService.login(this.loginForm.value)
      .pipe(filter(result => result?.succeeded === true))
      .subscribe(() => this.router.navigateByUrl(this.returnUrl),
        error => { console.log(error); this.loginForm.enable(); }).add(() => this.isBeingLoggedIn = false);
  }

  fillSuperAdminCredentials() {
    this.loginForm = new UntypedFormGroup({
      email: new UntypedFormControl('superadmin@e-smart.com', Validators.required && Validators.email),
      password: new UntypedFormControl('123Pa$$word!', Validators.required)
    });
  }

  fillStaffCredentials() {
    this.loginForm = new UntypedFormGroup({
      email: new UntypedFormControl('staff@e-smart.com', Validators.required && Validators.email),
      password: new UntypedFormControl('123Pa$$word!', Validators.required)
    });
  }

  getConfigValue(key: string): any {
    return getDeepFromObject(this.options, key, null);
  }

 
}
