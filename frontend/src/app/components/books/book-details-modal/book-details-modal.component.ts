import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { BookComponent } from '../book/book.component';
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
  @Input() comments: any[]=[];
  @Output() close = new EventEmitter<void>();
  @Output() addedComment = new EventEmitter<any>();
  commentControl=new FormControl();
  closeModal() {
    this.close.emit();
  }
  addComment(){
    this.addedComment.emit({id:this.book.id,text:this.commentControl.value});
    this.commentControl.reset();
  }
}