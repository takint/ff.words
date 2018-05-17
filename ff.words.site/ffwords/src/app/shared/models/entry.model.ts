export class EntryModel {
    public id: number;
    public title: string;
    public content: string;
    public currentStatus: EntryStatus;
    public createdUser: string;
    public createdDate: Date;
}

export enum EntryStatus
{
    Draft = 1,
    Pending = 2,
    Publish = 3,
    Hidden = 4,
    Closed = 5
};
