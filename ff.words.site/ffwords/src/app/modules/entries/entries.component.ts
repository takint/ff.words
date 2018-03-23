import { Component, OnInit } from '@angular/core';
import { EntriesService } from './entries.service';
import { AppUtils } from '../../shared/shared.utils';
import { Observable } from 'rxjs/Observable';
import { Promise } from 'q';

@Component({
    selector: 'app-entries',
    templateUrl: './entries.component.html',
    styleUrls: ['./entries.component.css']
})
export class EntriesComponent implements OnInit {

    listData;

    constructor(private service: EntriesService) {
        //this.service.getData(`${AppUtils.apiHost}api/Entry/GetEntries`)
        //    .subscribe(response => {
        //        if (response) {
        //            console.log(response);
        //        }
        //    });
        this.listData = this.getList();
    }

    getList() {
        return this.service.getData(`${AppUtils.apiHost}api/Entry/GetEntries`).toPromise<any>();
    }

    ngOnInit() {

    }

}
