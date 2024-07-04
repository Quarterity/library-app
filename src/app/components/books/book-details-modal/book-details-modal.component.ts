import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { BookComponent } from '../book/book.component';
import { StateService } from '../../../services/state.service';

@Component({
  selector: 'app-book-details-modal',
  standalone: true,
  imports: [CommonModule, BookComponent],
  templateUrl: './book-details-modal.component.html',
  styleUrl: './book-details-modal.component.css'
})
export class BookDetailsModalComponent {
  @Input() book:any;
  @Input() isOpen: boolean = false;
  @Input() isBookLiked=false;
  @Output() close = new EventEmitter<void>();
  closeModal() {
    this.close.emit();
  }
}
