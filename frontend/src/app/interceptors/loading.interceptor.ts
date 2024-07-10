import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse } from "@angular/common/http";
import { Injectable } from "@angular/core"
import { Observable, catchError, finalize, tap, throwError } from "rxjs";
import { LoadingService } from "../services/loading.service";

@Injectable()
export class LoadingInterceptor implements HttpInterceptor{
    
    constructor(private loadingService:LoadingService) {}

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        this.loadingService.showLoader();
        this.loadingService.hideError();
        return next.handle(req).pipe(
            tap((event:HttpEvent<any>)=>{
                if(event instanceof HttpResponse){
                    this.loadingService.hideLoader();
                }
            }),
            catchError((error:HttpErrorResponse)=>{
                this.loadingService.showError(error);
                this.loadingService.hideLoader();
                return throwError(()=> new Error(error.message));
            }),
            finalize(()=>{
                this.loadingService.hideLoader();
            })
        );
    }

}

