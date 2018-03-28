import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { FormsModule } from '@angular/forms';
import { EntriesComponent } from './entries.component';
import { EntryComponent } from './entry/entry.component';
import { EntriesService } from './entries.service';

const ENTRIES_ROUTE = [
    { path: '', redirectTo: 'list', pathMatch: 'full' },
    { path: 'list', component: EntriesComponent },
    { path: ':id', component: EntryComponent }
];

@NgModule({
    imports: [
        CommonModule,
        RouterModule.forChild(ENTRIES_ROUTE),
        NgxDatatableModule,
        FormsModule
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
