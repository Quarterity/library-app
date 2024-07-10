import { HttpClient, HttpErrorResponse, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, catchError, throwError } from "rxjs";
import { environment } from "../../environments/environment";


@Injectable({
    providedIn:"root"
})
export class BookService{
    constructor(private httpClient: HttpClient) { }

    getBooks(pageNumber:number, pageSize:number,searchParam:string=''):Observable<any>{
        return this.httpClient
        .get(`${environment.apiUrl}/book?page=${pageNumber}&pageSize=${pageSize}&searchParam=${searchParam}`)
        .pipe(catchError(this.errorHandler));
    }
    getComments(bookId: number): Observable<Comment[]> {
        return this.httpClient
        .get<any>(`${environment.apiUrl}/book/${bookId}/comments`)
        .pipe(catchError(this.errorHandler));
    }
    addComment(bookId: number, comment: string): Observable<Comment> {
        const httpOptions = {
            headers: new HttpHeaders({
              'Content-Type': 'application/json'
            })
          };
        return this.httpClient
        .post<any>(`${environment.apiUrl}/book/${bookId}/comment`, JSON.stringify(comment), httpOptions)
        .pipe(catchError(this.errorHandler));
    }
    errorHandler(err:HttpErrorResponse) {
        console.error("An error occured:", err.message);
        return throwError(()=>new Error("Error occured"));
    }
}
