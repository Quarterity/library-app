import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { LikeButtonComponent } from '../../shared/like-button/like-button.component';

@Component({
  selector: 'app-book',
  standalone: true,
  imports: [CommonModule,LikeButtonComponent],
  templateUrl: './book.component.html',
  styleUrl: './book.component.css'
})
export class BookComponent {
    @Input() book:any;
    @Input() isModalAvailable=false;
    @Output() titleClicked=new EventEmitter<any>;

    openModal(book: any) {
      if(this.isModalAvailable)
        this.titleClicked.emit(book);
    }
}
