import { Component, OnInit } from '@angular/core';
import { BookService } from '../../services/book.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-books-home',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './books-home.component.html',
  styleUrl: './books-home.component.css'
})
export class BooksHomeComponent implements OnInit {
  private _booksService:BookService;
  public booksList:any;
  constructor(private bookService:BookService) {
    this._booksService=bookService;
  }
  ngOnInit(): void {
      this._booksService.getBooks().subscribe(res=>{
        this.booksList=res;
      })
  }
}
