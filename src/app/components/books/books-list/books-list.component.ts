import { Component, EventEmitter, Input, Output } from '@angular/core';
import { BookComponent } from '../book/book.component';
import { CommonModule } from '@angular/common';
import { BookDetailsModalComponent } from '../book-details-modal/book-details-modal.component';
import { StateService } from '../../../services/state.service';
import { state } from '@angular/animations';

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
  likedBooksList:number[]=[];
  
  constructor(private stateService:StateService) {
  }
  openModal(data:any){
    this.titleClick.emit(data);
  }
  ngOnInit(){
    this.stateService.likedBooks$.subscribe(res=>{
      this.likedBooksList=res;
    })
  }

  isBookLiked(id:any){
    return this.likedBooksList.includes(id);
  }
}
