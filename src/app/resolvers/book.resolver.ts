import { inject } from '@angular/core';
import { ResolveFn } from '@angular/router';
import { BookService } from '../services/book.service';

export const bookResolver: ResolveFn<any> = () => {
  return inject(BookService).getBooks();
};
