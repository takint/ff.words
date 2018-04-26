import { Component, OnInit, ViewChild, AfterViewChecked } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Form, NgForm } from '@angular/forms';
import { EntriesService } from '../entries.service';
import { BaseFormComponent } from '../../../shared/shared.component';
import { AppUtils } from '../../../shared/shared.utils';
import { EntryModel } from '../../../shared/models/entry.model';
import { NgControl } from '@angular/forms/src/directives/ng_control';

@Component({
    selector: 'app-entry',
    templateUrl: './entry.component.html',
    styleUrls: ['./entry.component.css']
})
export class EntryComponent extends BaseFormComponent implements AfterViewChecked, OnInit {

    public entryPromise: Promise<any> = null;

    constructor(protected route: ActivatedRoute, protected service: EntriesService, private router: Router) {
        super(route, service);
    }

    loadModelForEditMode(id) {
        this.entryPromise = this.service.getEntry(id);
        this.entryPromise.then(resolved => {
            if (!AppUtils.isNullOrEmpty(resolved)) {
                this.model = resolved;
                this.isInitDataLoaded = true;
            }
        });
    }

    loadModelForAddMode() {
        this.model = new EntryModel();
        this.isInitDataLoaded = true;
    }

    ngOnInit() {
        super.ngOnInit();
    }

    ngAfterViewChecked(): void {
        super.ngAfterViewChecked();
    }

    onSubmit() {
        super.onSubmit();
    }

    addNewModel() {
        this.service.postData(`${AppUtils.apiHost}Entry/AddEntry`, this.model)
            .subscribe((data) => {
                this.router.navigate([`/entry/list`]);
            });
    }

    updateModel() {
        this.service.postData(`${AppUtils.apiHost}Entry/UpdateEntry`, this.model)
            .subscribe((data) => {
                this.router.navigate([`/entry/list`]);
            });
    }

    // Validation
    validationRules = {
        'entryTitle': {
            'required': 'Title is required'
        }
    };
}
