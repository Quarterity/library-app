import { Component, EventEmitter, Input, Output, SimpleChanges } from '@angular/core';
import { BookComponent } from '../book/book.component';
import { CommonModule } from '@angular/common';
import { BookDetailsModalComponent } from '../book-details-modal/book-details-modal.component';

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
  @Input() totalPages:number=0;
  @Input() currentPage:number=1;
  @Output() pageChanged=new EventEmitter<any>;
  @Output() titleClick=new EventEmitter<any>;

  openModal(data:any){
    this.titleClick.emit(data);
  }
  isBookLiked(id:any){
    return this.likedBooksList.includes(id);
  }
  onPageChange(page: number): void {
    this.pageChanged.emit(page);
    window.scroll({
      top: 0,
      behavior: 'smooth'
    });  }
}
