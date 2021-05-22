import { FullAdPhotoItem } from './full-ad-photo-Item';

export class FullAd {
  public id: string;
  public name: string;
  public locationText: string;
  public price: number;
  public photos: FullAdPhotoItem[];
  public ownerName: string;
  public ownerNumber: string;
  public ownerPhoto: string;
  public text: string;
  public status: string;
  public moderationComment: string;
  public ownerId: string;
}
