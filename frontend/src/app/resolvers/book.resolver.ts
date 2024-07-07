import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, ResolveFn } from '@angular/router';
import { BookService } from '../services/book.service';

export const bookResolver: ResolveFn<any> = (route: ActivatedRouteSnapshot) => {
  const page = route.queryParams['page'] || 1;
  const pageSize = route.queryParams['pageSize'] || 10;
  return inject(BookService).getBooks(+page,+pageSize);
};
