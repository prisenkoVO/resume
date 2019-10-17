import {
    trigger,
    transition,
    style,
    query,
    animateChild,
    animate,
    keyframes,
} from '@angular/animations';

//Basic
export const fader = 
    trigger('routeAnimations', [
        transition('* <=> *', [
            query(':enter, :leave', [
                style({
                    position: 'absolute',
                    left: 0,
                    width: '100%',
                    opacity: 0,
                    transform: 'scale(0) translatey(100%)',

                })
            ]),
            query(':enter', [
                animate('600ms ease', 
                    style({ opacity: 1, transform: 'scale(1) translatey(0)' })
                )
            ])
        ])
    ])


