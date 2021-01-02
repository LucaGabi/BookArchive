import { IAPIResponse } from './IAPIResponse';
import { Observable } from 'rxjs';
export interface IAPIDataService<TEntity, TKey> {
    getAll(): Observable<IAPIResponse<TEntity[]>>;
    getOneById(id: TKey): Observable<IAPIResponse<TEntity>>;
    create(entity: TEntity): Observable<IAPIResponse<TEntity>>;
    update(entity: TEntity): Observable<IAPIResponse<TEntity>>;
    delete(entity: TKey): Observable<IAPIResponse<TEntity>>;

}