import { ViewChild, AfterViewChecked, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs/Rx';
import { AppUtils } from './shared.utils';
import { AppService } from '../app.service';

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

    constructor(protected route: ActivatedRoute, protected service: AppService) {
        super();
        this.routingHandler();
    }

    ngOnInit() { }

    onSubmit() {
        let errorMsg = '';

        if (this.mainForm.invalid) {
            errorMsg = 'Invalid model';
        } else {
            errorMsg = this.customValidationOnSubmit();
        }

        if (AppUtils.isNullOrEmpty(errorMsg)) {
            if (this.isAddMode) {
                this.addNewModel();
            } else { // Update mode
                this.updateModel();
            }
        } else {
            this.validateAllFields(false);
        }
    }

    addNewModel() {

    }

    updateModel() {

    }

    onValueChanged(data?: any) {
        this.validateAllFields(true);
    }

    routingHandler() {
        this.route.params.subscribe(params => {
            if (!AppUtils.isNullOrEmpty(params['id'])) {
                let entityId = params['id'];
                if (entityId > 0) {
                    this.isEditMode = true;
                    this.initModelForEditMode(entityId);
                } else {
                    this.isAddMode = true;
                    this.initModelForAddMode();
                }
            }
        });
    };

    initModelForAddMode() { }

    initModelForEditMode(id: number) {
    }

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

    customValidationOnSubmit(): string { return null; }

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
