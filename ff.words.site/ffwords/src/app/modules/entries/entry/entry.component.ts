import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Form } from '@angular/forms';
import { EntriesService } from '../entries.service';
import { AppUtils } from '../../../shared/shared.utils';
import { EntryModel } from '../../../shared/models/entry.model';

@Component({
    selector: 'app-entry',
    templateUrl: './entry.component.html',
    styleUrls: ['./entry.component.css']
})
export class EntryComponent implements OnInit {

    public entryPromise: Promise<any> = null;
    private model: EntryModel;

    constructor(private route: ActivatedRoute,
        private service: EntriesService) {
        this.route.params.subscribe(params => {
            if (!AppUtils.isNullOrEmpty(params['id'])) {
                let entryId = params['id'];
                this.entryPromise = this.service.getEntry(entryId);
                this.entryPromise.then(resolved => {
                    if (!AppUtils.isNullOrEmpty(resolved)) {
                        this.model = resolved;
                    }
                });
            }
        });
    }

    ngOnInit() {
    }

}
