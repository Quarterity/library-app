import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output, SimpleChanges } from '@angular/core';
import { BooksListComponent } from '../books-list/books-list.component';
import { SearchComponent } from '../../shared/search/search.component';
import { BookDetailsModalComponent } from '../book-details-modal/book-details-modal.component';
import { StateService } from '../../../services/state.service';
import { BookService } from '../../../services/book.service';

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
  comments: any[]=[];
  constructor(private stateService:StateService, private bookService:BookService) {}

  ngOnInit(){
    this.stateService.likedBooks$.subscribe(res=>{
      this.likedBooksList=res;
    })
  }
  openModal(data:any){
    this.getComments(data.id);
    this.selectedBook = data;
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
  getComments(id:number) {
    this.bookService.getComments(id).subscribe(res=>{
      this.comments=res.reverse();
      this.isModalOpen=true;
    });  
  }
  addComment(commentData:any){
    this.bookService.addComment(commentData.id,commentData.text).subscribe(res=>{
      this.getComments(commentData.id);
    });
  }
}
