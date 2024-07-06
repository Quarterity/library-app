import { Injectable } from '@angular/core';
import { BehaviorSubject, Subject } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class StateService {
  public likedBooksSubject=new BehaviorSubject<number[]>([]);
  likedBooks$=this.likedBooksSubject.asObservable();
  likedBooksStorageArray:number[]=[];

  constructor() {
    this.loadLikedbooks();

  }
  loadLikedbooks(){
    let likedBooksStorageObject=localStorage.getItem(environment.localStorageKeys.likedBooksKey);
    this.likedBooksStorageArray= likedBooksStorageObject? JSON.parse(likedBooksStorageObject):[];
    this.likedBooksSubject.next(this.likedBooksStorageArray);
  }

  toggleLike(id:number){
     this.likedBooksStorageArray.includes(id)
      ? this.likedBooksStorageArray = this.likedBooksStorageArray.filter(s=>s!=id)
      : this.likedBooksStorageArray.push(id);
    localStorage.setItem(environment.localStorageKeys.likedBooksKey,JSON.stringify(this.likedBooksStorageArray));
    this.likedBooksSubject.next(this.likedBooksStorageArray);
  }
}
