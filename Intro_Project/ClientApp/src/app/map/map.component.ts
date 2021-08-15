import { Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { loadModules } from 'esri-loader';
import esri = __esri;

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit {
  // @Output() mapLoadedEvent = new EventEmitter<boolean>();

  // // The <div> where we will place the map
  // @ViewChild("mapView", { static: true }) private mapViewEl: ElementRef;

  // /**
  //  * _zoom sets map zoom
  //  * _center sets map center
  //  * _basemap sets type of map
  //  * _loaded provides map loaded status
  //  */
  // private _zoom = 10;
  // private _center: Array<number> = [0.1278, 51.5074];
  // private _basemap = "streets";
  // private _loaded = false;
  // private _view: esri.MapView = null;

  // get mapLoaded(): boolean {
  //   return this._loaded;
  // }

  // @Input()
  // set zoom(zoom: number) {
  //   this._zoom = zoom;
  // }

  // get zoom(): number {
  //   return this._zoom;
  // }

  // @Input()
  // set center(center: Array<number>) {
  //   this._center = center;
  // }

  // get center(): Array<number> {
  //   return this._center;
  // }

  // @Input()
  // set basemap(basemap: string) {
  //   this._basemap = basemap;
  // }

  // get basemap(): string {
  //   return this._basemap;
  // }

  // constructor() {}

  // async initializeMap() {
  //   try {
  //     // Load the modules for the ArcGIS API for JavaScript
  //     const [EsriMap, EsriMapView] = await loadModules([
  //       "esri/Map",
  //       "esri/views/MapView"
  //     ]);

  //     // Configure the Map
  //     const mapProperties: esri.MapProperties = {
  //       basemap: this._basemap
  //     };

  //     const map: esri.Map = new EsriMap(mapProperties);

  //     // Initialize the MapView
  //     const mapViewProperties: esri.MapViewProperties = {
  //       container: this.mapViewEl.nativeElement,
  //       center: this._center,
  //       zoom: this._zoom,
  //       map: map
  //     };

  //     this._view = new EsriMapView(mapViewProperties);
  //     await this._view.when();
  //     return this._view;
  //   } catch (error) {
  //     console.log("EsriLoader: ", error);
  //   }
  // }

  // ngOnInit() {
  //   // Initialize MapView and return an instance of MapView
  //   this.initializeMap().then(mapView => {
  //     // The map has been initialized
  //     console.log("mapView ready: ", this._view.ready);
  //     this._loaded = this._view.ready;
  //     this.mapLoadedEvent.emit(true);
  //   });
  // }

  // ngOnDestroy() {
  //   if (this._view) {
  //     // destroy the map view
  //     this._view.container = null;
  //   }
  // }





  @Output() mapLoadedEvent = new EventEmitter<boolean>();
  
  @Input() public latitude:number;// = 35.892722;
  @Input() public longitude:number; //= 14.430219;
  @Input() public zoomLevel:number; //= 7;
  private center = [0.1278, 51.5074];
  private _loaded : boolean = false;

  //#region setters and getters
  // @Input()
  // set setZoomlevel(value:number) {
  //   this.zoomLevel = value;
  // }

  // get getZoomlevel():number{
  //   return this.zoomLevel;
  // }

  // @Input()
  // set setLongitude(value:number){
  //   this.longitude = value;
  // }
  
  // get getLongitude():number{
  //   return this.longitude;
  // }

  // @Input()
  // set setLatitude(value:number){
  //   this.latitude = value;
  // }
  
  // get getLatitude():number{
  //   return this.latitude;
  // }
  //#endregion
 
  private _basemap = "streets";
  private _view: esri.MapView = null;

  @ViewChild("mapView",{static:true}) private map:ElementRef;

  constructor() { 
  }

  async initMap(){
    try {
      // Load the modules for the ArcGIS API for JavaScript
      const [EsriMap, EsriMapView] =await loadModules([
        "esri/Map",
        "esri/views/MapView"
      ]);

      // Configure the Map
      const mapProperties: esri.MapProperties = {
        basemap: this._basemap
      };

      const map: esri.Map = new EsriMap(mapProperties);

      // Initialize the MapView
      const mapViewProperties: esri.MapViewProperties = {
        container: this.map.nativeElement,
        center: this.center,
        zoom: this.zoomLevel,
        map: map
      };

      this._view = new EsriMapView(mapViewProperties);
      await this._view.when();
      return this._view;
    } catch (error) {
      console.log("EsriLoader: ", error);
    }
  }

  ngOnInit() {
    this.initMap().then(mapView => {
      // The map has been initialized
      console.log("mapView ready: ", this._view.ready);
       this._loaded = this._view.ready;
       this.mapLoadedEvent.emit(true);
    });
  }

}
