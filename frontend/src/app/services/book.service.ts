import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, catchError, throwError } from "rxjs";
import { environment } from "../../environments/environment";


@Injectable({
    providedIn:"root"
})
export class BookService{
    constructor(private httpClient: HttpClient) { }

    getBooks():Observable<any>{
        return this.httpClient.get(environment.apiUrl+"/books").pipe(catchError(this.errorHandler));
    }
    
    errorHandler(err:HttpErrorResponse) {
        console.error("An error occured:", err.message);
        return throwError(()=>new Error("Error occured"));
    }
}
