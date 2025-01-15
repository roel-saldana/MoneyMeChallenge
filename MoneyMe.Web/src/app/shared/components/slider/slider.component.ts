import { CurrencyPipe } from '@angular/common';
import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core'; 

@Component({ 
  selector: 'app-slider', 
  templateUrl: './slider.component.html', 
  styleUrls: ['./slider.component.css'],
  standalone: false
}) 

export class SliderComponent implements OnInit { 
  @Input() isCurrency: boolean;
  @Input() minValue: number;
  @Input() maxValue: number;
  @Input() sliderValue: number; 
  @Input() fieldLabelValue: string;  
  @Output() sliderValueChange: EventEmitter<number> = new EventEmitter<number>();

  constructor(public currencyPipe: CurrencyPipe) { }

  ngOnInit() :void {
    this.sliderValue = this.sliderValue;
  }

  updateValue(event: Event) { 
    const input = event.target as HTMLInputElement; 
    const numericValue = Number(input.value);
    if (!isNaN(numericValue)) {
      this.sliderValue = numericValue; 
      this.sliderValueChange.emit(this.sliderValue); 
    }
  }
}