import { Component, OnInit, ViewChild, AfterViewChecked } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Form, NgForm } from '@angular/forms';
import { EntriesService } from '../entries.service';
import { AppUtils } from '../../../shared/shared.utils';
import { EntryModel } from '../../../shared/models/entry.model';
import { NgControl } from '@angular/forms/src/directives/ng_control';

@Component({
    selector: 'app-entry',
    templateUrl: './entry.component.html',
    styleUrls: ['./entry.component.css']
})
export class EntryComponent implements AfterViewChecked, OnInit {

    public entryPromise: Promise<any> = null;
    public entryLoaded: boolean = false;
    public model: EntryModel;

    mainForm: NgForm;
    @ViewChild('mainForm') currentForm: NgForm;

    constructor(private route: ActivatedRoute,
        private service: EntriesService) {

        this.route.params.subscribe(params => {
            if (!AppUtils.isNullOrEmpty(params['id'])) {
                let entryId = params['id'];
                this.entryPromise = this.service.getEntry(entryId);
                this.entryPromise.then(resolved => {
                    if (!AppUtils.isNullOrEmpty(resolved)) {
                        this.model = resolved;
                        this.entryLoaded = true;
                    }
                });
            }
        });
    }

    ngOnInit() {
    }

    ngAfterViewChecked(): void {
        this.formChanged();
    }

    formErrors = {};
    validationRules = {};

    formChanged() {
        if (this.currentForm === this.mainForm) { return; }
        this.mainForm = this.currentForm;

        if (!AppUtils.isNullOrEmpty(this.mainForm)) {
            this.mainForm.valueChanges.subscribe(
                (data) => {
                    this.onValueChanged(data);
                });
        }
    }

    validateAllFields(checkDirty: boolean) {
        if (AppUtils.isNullOrEmpty(this.mainForm)) { return; }

        const form = this.mainForm.form;
        for (const field in this.validationRules) {
            this.formErrors[field] = '';
            const control = form.get(field);

            if (!AppUtils.isNullOrEmpty(control)) {
                if (!control.valid && (control.dirty || checkDirty)) {
                    const message = this.validationRules[field];
                    for (const key in control.errors) {
                        this.formErrors[field] += message[key] + ' ';
                    }
                    control.markAsDirty();
                }
            }
        }
    }

    onValueChanged(data?: any) {
        this.validateAllFields(true);
    }

    onSubmit() {
        this.service.postData(`${AppUtils.apiHost}Entry/UpdateEntry`, this.model)
            .subscribe((data) => {
                window.location.reload();
            });
    }
}
