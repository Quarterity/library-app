import { Component, EventEmitter, Input, Output, SimpleChanges } from '@angular/core';
import { BookComponent } from '../book/book.component';
import { CommonModule } from '@angular/common';
import { BookDetailsModalComponent } from '../book-details-modal/book-details-modal.component';
import { StateService } from '../../../services/state.service';

@Component({
  selector: 'app-books-list',
  standalone: true,
  imports: [BookComponent,CommonModule, BookDetailsModalComponent],
  templateUrl: './books-list.component.html',
  styleUrl: './books-list.component.css'
})
export class BooksListComponent {
  @Input() booksList:any[]=[];
  @Input() searchParam:string="";
  @Input() likedBooksList:number[]=[];
  @Output() titleClick=new EventEmitter<any>;
  filteredBooks:any[]=[];

  openModal(data:any){
    this.titleClick.emit(data);
  }
  ngOnInit(){
    this.filteredBooks=this.booksList;
  }
  ngOnChanges(changes:SimpleChanges){
    if(changes['searchParam']){
      this.filteredBooks=this.booksList.filter(s=>s.title.toLowerCase().includes(this.searchParam.toLowerCase()));
    }
  }

  isBookLiked(id:any){
    return this.likedBooksList.includes(id);
  }
}
