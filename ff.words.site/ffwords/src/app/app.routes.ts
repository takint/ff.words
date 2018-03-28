import { Routes, RouterModule } from '@angular/router';

const BASE_ROUTES: Routes = [
    { path: '', redirectTo: 'entry', pathMatch: 'full' },
    { path: 'entry', loadChildren: './modules/entries/entries.module#EntriesModule' },
];

export const routing = RouterModule.forRoot(BASE_ROUTES);
