import { Component, OnInit, Input, Output, ViewChild, ViewContainerRef, ComponentFactoryResolver, ChangeDetectorRef } from '@angular/core';

//  Service imports
import { UtilityService } from '@core/utility/utility.service';

// Import models
import { User } from '@app/shared/interfaces/iUser';
import { fromEvent } from 'rxjs';

@Component({
  selector: 'app-authpage',
  templateUrl: './authpage.component.html',
  styleUrls: ['./authpage.component.scss']
})
export class AuthpageComponent implements OnInit {

  @ViewChild('authPage', { read: false, static: true }) authPage;
  @ViewChild('errormessage', { read: ViewContainerRef, static: false }) errorWindow: ViewContainerRef;
  
  public bgLogoStyle: object = {}; // Blur bg of  Auth page
  public authContentStyle: object = {
    opacity: ''
  };

  private user: User;
  public showPreloader = false;
  public showAuthForm = true;
  public bgTransform: string;

  private mouseX: number;
  private mouseY: number;

  private bgLogoAnimation = 'showBgLogo';

  constructor(
    private utility: UtilityService,
    private resolver: ComponentFactoryResolver,
    private cdr: ChangeDetectorRef,
  ) { }

  ngOnInit() {
    // Delete header and footer from page
    const header = document.querySelector('header');
    const footer = document.querySelector('footer');
    const nav = document.querySelector('nav')
    header.style.display = 'none';
    footer.style.display = 'none';
    nav.style.display = 'none';

    // Delete cookies
    this.utility.deleteCookie('token');
    this.utility.deleteCookie('name');

    // If user was authed then dont show the animation
    const isWasAuth: string | boolean = window.localStorage.getItem('auth');
    if(isWasAuth) {
      this.bgLogoAnimation = ' ';
    }

    setTimeout(() => {
      // Animate page here
      this.animatePage();

      // Move background image
      fromEvent(this.authPage.nativeElement, 'mousemove')
        .subscribe(() => {
          this.moveBg(this.mouseX, this.mouseY);
        });
    }, 1800);
  }

  ngOnDestroy(): void {
    const header = document.querySelector('header');
    const footer = document.querySelector('footer');
    const nav = document.querySelector('nav')
    header.style.display = 'block';
    footer.style.display = 'block';
    nav.style.display = 'flex';
  }

  animatePage() {
    // Check if this user was authed
    

    // Set the correct styles
    this.bgLogoStyle = {
      zIndex: '0',
      filter: 'blur(4px)'
    };

    this.authContentStyle = {
      opacity: '1',
      zIndex: '2',
    };
  }

  // Get last position of mouse and set it to fields
  getMousePos($event: MouseEvent): void {
    this.mouseX = Math.round($event.clientX);
    this.mouseY = Math.round($event.clientY);
  }


  // Moves image on the background
  moveBg(mouseX: number, mouseY: number) {
    const startX = 100;
    const startY = -100;
    const w = document.documentElement.offsetWidth;
    const h = document.documentElement.offsetHeight;

    const posX = Math.round(mouseX / w * startX);
    const posY = Math.round(mouseY / h * startY);

    this.bgTransform = `translate( ${posX}px , ${posY}px)`;
  }

  onError(err) {
    this.utility.handleHttpError(err)
  }
}