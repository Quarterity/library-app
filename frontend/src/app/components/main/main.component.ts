import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from '../shared/header/header.component';
import { FooterComponent } from '../shared/footer/footer.component';
import { BooksHomeComponent } from '../books/books-home/books-home.component';
import { ActivatedRoute, Router } from '@angular/router';
import { BookService } from '../../services/book.service';
import { LoadingService } from '../../services/loading.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-main',
  standalone: true,
  imports: [CommonModule, HeaderComponent,FooterComponent, BooksHomeComponent],
  templateUrl: './main.component.html',
  styleUrl: './main.component.css'
})
export class MainComponent implements OnInit {
  booksData: any[]=[];

  booksCount: any;
  totalPages: any;
  currentPage: any;
  pageSize:number=10;
  searchParam: string="";
  loading$: Observable<boolean>;
  error$: Observable<string|null>;
  success$: Observable<string|null>;
  constructor(private route:ActivatedRoute,private router: Router, private bookService: BookService, private loadingService:LoadingService) {
    this.loading$=this.loadingService.loading$;
    this.error$=this.loadingService.error$;
    this.success$=this.loadingService.success$;
  }
  ngOnInit(): void {
    this.route.data.subscribe(data => {//save data returned from getbooks initial request of resolver
      const response = data['books'];
      this.booksData = response.books;
      this.booksCount = response.booksCount;
      this.totalPages = response.pagesCount;
      this.currentPage = response.currentPage;
    });
    this.route.queryParams.subscribe(params=>{//on page change, update current page, size and fetch new books data with them
      this.currentPage = +params['page'] || 1;
      this.pageSize = +params['pageSize'] || 10;
      this.searchParam = params['searchParam'];
      this.fetchBooks();
    })
  }

  private fetchBooks(): void {
    this.bookService.getBooks(this.currentPage, this.pageSize,this.searchParam).subscribe(response => {
      this.booksData = response.books;
      this.booksCount = response.booksCount;
      this.totalPages = response.pagesCount;
      this.currentPage = response.currentPage;      
    });
  } 
  onSearchInputChange(data:string){
    this.searchParam=data;
    this.onPageChange(1);
  }

  onPageChange(page: number): void {
    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: { page: page, pageSize: this.pageSize, searchParam:this.searchParam},
      queryParamsHandling: 'merge'
    });
  }
  
}
