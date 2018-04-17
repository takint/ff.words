import { ViewChild, AfterViewChecked, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { AppUtils } from './shared.utils';

export class BaseComponent {
}

export class BaseFormComponent extends BaseComponent implements AfterViewChecked, OnInit {

    mainForm: NgForm;
    @ViewChild('mainForm') currentForm: NgForm;

    public isInitDataLoaded: boolean = false;
    public model: any;

    public isAddMode: boolean;
    public isEditMode: boolean;

    // Validation rules
    formErrors = {};
    validationRules = {};

    constructor(protected route: ActivatedRoute) {
        super();
    }

    ngOnInit() { }

    onSubmit() { }

    onValueChanged(data?: any) {
        this.validateAllFields(true);
    }

    routingHandler() {
        this.route.params.subscribe(params => {
            if (!AppUtils.isNullOrEmpty(params['id'])) {
                let entryId = params['id'];
                if (entryId > 0) {
                    // Load init data

                    //this.entryPromise = this.service.getEntry(entryId);
                    //this.entryPromise.then(resolved => {
                    //    if (!AppUtils.isNullOrEmpty(resolved)) {
                    //        this.model = resolved;
                    //        this.isInitDataLoaded = true;
                    //    }
                    //});
                } else {
                    this.isAddMode = true;
                    this.isInitDataLoaded = true;
                }
            }
        });
    };

    ngAfterViewChecked(): void {
        this.formChanged();
    }

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
}