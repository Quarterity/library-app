import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output, SimpleChange, SimpleChanges } from '@angular/core';
import { BookComponent } from '../book/book.component';
import { BookService } from '../../../services/book.service';
import { FormControl, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-book-details-modal',
  standalone: true,
  imports: [CommonModule, BookComponent,ReactiveFormsModule],
  templateUrl: './book-details-modal.component.html',
  styleUrl: './book-details-modal.component.css'
})
export class BookDetailsModalComponent {
  @Input() book:any;
  @Input() isOpen: boolean = false;
  @Input() isBookLiked=false;
  @Output() close = new EventEmitter<void>();
  commentControl=new FormControl();
  comments: any[]=[];
  constructor(private bookService:BookService) {}
  ngOnChanges(changes: SimpleChanges){
    if(changes['book']){
      this.getComments();
    }
  }
  getComments() {
    this.bookService.getComments(this.book.id).subscribe(res=>{
      this.comments=res.reverse();
    });  }
  closeModal() {
    this.close.emit();
  }
  addComment(){
    this.bookService.addComment(this.book.id,this.commentControl.value).subscribe(res=>{
      this.getComments();
    });
  }
}