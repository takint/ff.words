import { Component, OnInit } from '@angular/core';
import { EntriesService } from './entries.service';
import { AppUtils } from '../../shared/shared.utils';
import { Observable } from 'rxjs/Observable';
import { EntryModel, EntryStatus } from '../../shared/models/entry.model';

@Component({
    selector: 'app-entries',
    templateUrl: './entries.component.html',
    styleUrls: ['./entries.component.css']
})
export class EntriesComponent implements OnInit {

    public listPromise: Promise<any> = null;
    public entries: Array<EntryModel> = [];
    public isInitDataLoaded: boolean = false;

    constructor(private service: EntriesService) {
        this.listPromise = this.service.getList();
        this.listPromise
            .then(resolved => {
                if (!AppUtils.isNullOrEmpty(resolved)) {
                    this.entries = resolved;
                    this.isInitDataLoaded = true;
                }
            })
            .catch(reject => console.log(reject));
    }

    ngOnInit() {
    }
}
