import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EntriesComponent } from './entries.component';
import { EntryComponent } from './entry/entry.component';
import { EntriesService } from './entries.service';

@NgModule({
    imports: [
        CommonModule
    ],
    declarations: [
        EntriesComponent,
        EntryComponent
    ],
    providers: [
        EntriesService
    ]
})
export class EntriesModule { }
