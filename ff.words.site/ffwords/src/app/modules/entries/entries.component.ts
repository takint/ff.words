import { Component, OnInit } from '@angular/core';
import { EntriesService } from './entries.service';
import { AppUtils } from '../../shared/shared.utils';
import { Observable } from 'rxjs/Observable';
import { EntryModel } from '../../shared/models/entry.model';

@Component({
    selector: 'app-entries',
    templateUrl: './entries.component.html',
    styleUrls: ['./entries.component.css']
})
export class EntriesComponent implements OnInit {

    public listPromise: Promise<any> = null;
    public entries: Array<EntryModel> = [];

    constructor(private service: EntriesService) {
        this.service.getList().subscribe(data => {
            console.log(data);
            if (!AppUtils.isNullOrEmpty(data)) {
                this.entries = data;
            }
        });


        //this.listPromise.then(resolved => {
           
        //}).catch(reject => console.log(reject));
    }

    ngOnInit() {
    }
}
