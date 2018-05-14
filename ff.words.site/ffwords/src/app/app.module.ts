import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';
import { HTTP_INTERCEPTORS, HttpClient, HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { routing } from './app.routes';
import { VerticalMenuComponent } from './shared/components/vertical-menu/vertical-menu.component';
import { HeaderComponent } from './shared/components/header/header.component';
import { FooterComponent } from './shared/components/footer/footer.component';
import { JwtInterceptor } from './app.service';

@NgModule({
    declarations: [
        AppComponent,
        VerticalMenuComponent,
        HeaderComponent,
        FooterComponent
    ],
    imports: [
        HttpClientModule,
        BrowserModule,
        HttpModule,
        routing
    ],
    providers: [
        {
            provide: HTTP_INTERCEPTORS,
            useClass: JwtInterceptor,
            useFactory: '',
            deps: [HttpClient],
            multi: true
        }
    ],
    bootstrap: [
        AppComponent
    ]
})
export class AppModule { }
