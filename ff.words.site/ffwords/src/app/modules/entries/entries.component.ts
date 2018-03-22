import { Component, OnInit } from '@angular/core';
import { EntriesService } from './entries.service';
import { AppUtils } from '../../shared/shared.utils';

@Component({
    selector: 'app-entries',
    templateUrl: './entries.component.html',
    styleUrls: ['./entries.component.css']
})
export class EntriesComponent implements OnInit {

    constructor(private service: EntriesService) {
        this.service.getData(`${AppUtils.apiHost}api/Entry/GetEntries`)
            .subscribe(data => {
                if (data) {
                    console.log(data);
                }
            });
    }

    ngOnInit() {

    }

}
