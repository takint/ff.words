import { Routes, RouterModule } from '@angular/router';
import { EntriesComponent } from './modules/entries/entries.component';
import { EntryComponent } from './modules/entries/entry/entry.component';

export const BASE_ROUTES: Routes = [
    { path: '', redirectTo: 'entries', pathMatch: 'full' },
    { path: 'entries', component: EntriesComponent },
    { path: 'entry/:id', component: EntryComponent }
];

export const routing = RouterModule.forRoot(BASE_ROUTES);
