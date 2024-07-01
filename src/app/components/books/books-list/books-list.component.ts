import { Component, EventEmitter, Input, Output } from '@angular/core';
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
  @Input() booksList:any=null;
  @Output() titleClick=new EventEmitter<any>;

  openModal(data:any){
    this.titleClick.emit(data);
  }
}
