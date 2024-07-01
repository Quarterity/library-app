import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { BooksListComponent } from '../books-list/books-list.component';
import { SearchComponent } from '../../shared/search/search.component';
import { BookDetailsModalComponent } from '../book-details-modal/book-details-modal.component';

@Component({
  selector: 'app-books-home',
  standalone: true,
  imports: [SearchComponent, BooksListComponent,CommonModule,BookDetailsModalComponent],
  templateUrl: './books-home.component.html',
  styleUrl: './books-home.component.css'
})
export class BooksHomeComponent {
  selectedBook: any = null;
  isModalOpen = false;
  @Input() books=null;
  
  openModal(data:any){
    this.selectedBook = data;
    this.isModalOpen = true;
  }
  closeModal() {
    this.isModalOpen = false;
  }
}
