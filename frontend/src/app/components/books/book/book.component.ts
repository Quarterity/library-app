import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { LikeButtonComponent } from '../../shared/like-button/like-button.component';
import { StateService } from '../../../services/state.service';

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
    @Input() isLiked:any;
    @Output() titleClicked=new EventEmitter<any>;

    constructor(private stateService:StateService) {}
    
    openModal(book: any) {
      if(this.isModalAvailable)
        this.titleClicked.emit(book);
    }
    
    toggleLike(){
      this.stateService.toggleLike(this.book.id);
    }
}
