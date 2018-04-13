import { Component, OnInit, ViewChild, AfterViewChecked } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
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
    public model: EntryModel;

    constructor(protected route: ActivatedRoute, protected service: EntriesService) {

        super(route);

        this.route.params.subscribe(params => {
            if (!AppUtils.isNullOrEmpty(params['id'])) {
                let entryId = params['id'];
                this.entryPromise = this.service.getEntry(entryId);
                this.entryPromise.then(resolved => {
                    if (!AppUtils.isNullOrEmpty(resolved)) {
                        this.model = resolved;
                        this.isInitDataLoaded = true;
                    }
                });
            }
        });
    }

    ngOnInit() {
        super.ngOnInit();
    }

    ngAfterViewChecked(): void {
        super.ngAfterViewChecked();
    }

    onSubmit() {
        this.service.postData(`${AppUtils.apiHost}Entry/UpdateEntry`, this.model)
            .subscribe((data) => {
                window.location.reload();
            });
    }
}
