import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output, SimpleChanges } from '@angular/core';
import { BooksListComponent } from '../books-list/books-list.component';
import { SearchComponent } from '../../shared/search/search.component';
import { BookDetailsModalComponent } from '../book-details-modal/book-details-modal.component';
import { StateService } from '../../../services/state.service';

@Component({
  selector: 'app-books-home',
  standalone: true,
  imports: [SearchComponent, BooksListComponent,CommonModule,BookDetailsModalComponent],
  templateUrl: './books-home.component.html',
  styleUrl: './books-home.component.css'
})
export class BooksHomeComponent {
  @Input() books:any[]=[];
  @Input()totalPages:number=0;
  @Input()currentPage:number=1;
  @Output() pageChanged=new EventEmitter<any>;
  @Output() searchInputChange=new EventEmitter<any>;

  selectedBook: any = null;
  isModalOpen = false;
  searchParam='';
  likedBooksList: number[]=[];
  constructor(private stateService:StateService) {}

  ngOnInit(){
    this.stateService.likedBooks$.subscribe(res=>{
      this.likedBooksList=res;
    })
  }
  openModal(data:any){
    this.selectedBook = data;
    this.isModalOpen = true;
  }
  closeModal() {
    this.isModalOpen = false;
  }
  isBookLiked(id:any){
    return this.likedBooksList.includes(id);
  }
  searchBooks(query:any){
    this.searchInputChange.emit(query);
  }
  onPageChange(page: number): void {
    this.pageChanged.emit(page);
  }
}
