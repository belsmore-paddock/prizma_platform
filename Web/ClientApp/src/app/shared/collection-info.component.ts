import { Component, Input } from '@angular/core';
import { DocumentCollection } from 'ngx-jsonapi';
@Component({
    selector: 'collection-info',
    templateUrl: './collection-info.component.html'
})
export class CollectionInfoComponent {
    @Input() public collection: DocumentCollection;
}
