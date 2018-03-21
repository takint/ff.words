import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EntriesComponent } from './entries.component';
import { EntryComponent } from './entry/entry.component';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [EntriesComponent, EntryComponent]
})
export class EntriesModule { }
