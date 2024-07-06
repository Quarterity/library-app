import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from '../shared/header/header.component';
import { FooterComponent } from '../shared/footer/footer.component';
import { BooksHomeComponent } from '../books/books-home/books-home.component';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-main',
  standalone: true,
  imports: [CommonModule, HeaderComponent,FooterComponent, BooksHomeComponent],
  templateUrl: './main.component.html',
  styleUrl: './main.component.css'
})
export class MainComponent implements OnInit {
  booksData: any;
  constructor(private route:ActivatedRoute) {
    
  }
  ngOnInit(): void {
    this.booksData = this.route.snapshot.data['books'];
  }
  
}
