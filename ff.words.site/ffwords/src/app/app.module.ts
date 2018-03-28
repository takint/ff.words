import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { routing } from './app.routes';
import { VerticalMenuComponent } from './shared/components/vertical-menu/vertical-menu.component';
import { HeaderComponent } from './shared/components/header/header.component';
import { FooterComponent } from './shared/components/footer/footer.component';

@NgModule({
    declarations: [
        AppComponent,
        VerticalMenuComponent,
        HeaderComponent,
        FooterComponent
    ],
    imports: [
        BrowserModule,
        HttpModule,
        routing
    ],
    providers: [
    ],
    bootstrap: [
        AppComponent
    ]
})
export class AppModule { }
