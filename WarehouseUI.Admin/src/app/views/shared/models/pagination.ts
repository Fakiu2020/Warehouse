export  class FilterBase {
    page = 1;
    totalItems: number;
    pageSize = 10;
    Sort: string;
    SortDir:string;
    criteria = null; 
}

export class PagedResult<T> {
    entity: any;
    filters: FilterBase;

    constructor() {
        this.filters = new FilterBase();
    }
}
