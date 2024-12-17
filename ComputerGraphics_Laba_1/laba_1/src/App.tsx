import React, { useRef, useState } from 'react';
import logo from './logo.svg';
import { ColorResult, SketchPicker } from 'react-color';
import { Box, Grid, GridItem, Heading, NumberDecrementStepper, NumberIncrementStepper, NumberInput, NumberInputField, NumberInputStepper, SimpleGrid, Slider, SliderFilledTrack, SliderThumb, SliderTrack } from '@chakra-ui/react'
import ColorInput from './NumInput';
import { Console, log } from 'console';

function App() {
  const [color, setColor] = useState<string>('#fff');
  const [errorMessage, setErrorMessage] = useState<string>("none");
  const [errorMessageText, setErrorMessageText] = useState<string>("");

  const rgbToLab = (r: number, g: number, b: number): [number, number, number] => {
    
    const rgbToXyz = (r: number, g: number, b: number): [number, number, number] => {
      setErrorMessage("none")

      r = r / 255;
      g = g / 255;
      b = b / 255;
  
      r = r > 0.04045 ? Math.pow((r + 0.055) / 1.055, 2.4) : r / 12.92;
      g = g > 0.04045 ? Math.pow((g + 0.055) / 1.055, 2.4) : g / 12.92;
      b = b > 0.04045 ? Math.pow((b + 0.055) / 1.055, 2.4) : b / 12.92;
  
      const x = r * 0.4124 + g * 0.3576 + b * 0.1805;
      const y = r * 0.2126 + g * 0.7152 + b * 0.0722;
      const z = r * 0.0193 + g * 0.1192 + b * 0.9505;
  
      return [x * 100,y * 100, z * 100];
    };
  
    const xyzToLab = (x: number, y: number, z: number): [number, number, number] => {
      setErrorMessage("none")

      x /= 95.047;
      y /= 100.000;
      z /= 108.883;
  
      x = x > 0.008856 ? Math.pow(x, 1 / 3) : (7.787 * x) + (16 / 116);
      y = y > 0.008856 ? Math.pow(y, 1 / 3) : (7.787 * y) + (16 / 116);
      z = z > 0.008856 ? Math.pow(z, 1 / 3) : (7.787 * z) + (16 / 116);
  
      const l = Math.round((116 * y) - 16);
      const a = Math.round(500 * (x - y));
      const b =Math.round( 200 * (y - z));
  
      return [l, a, b];
    };
  
    const [x, y, z] = rgbToXyz(r, g, b);
    const lab = xyzToLab(x, y, z);
  
    if (lab.some(value => value < 0 || value > 100)) {
      setErrorMessage("block")
      setErrorMessageText(`RGB(${r}, ${g}, ${b}) converted to out-of-gamut LAB(${lab[0]}, ${lab[1]}, ${lab[2]})`)
      console.log(`RGB(${r}, ${g}, ${b}) converted to out-of-gamut LAB(${lab[0]}, ${lab[1]}, ${lab[2]})`);
    }
    
    return lab;
  };
  
  const labToRgb = (l: number, a: number, b: number): [number, number, number] => {
    const labToXyz = (l: number, a: number, b: number): [number, number, number] => {
      setErrorMessage("none")

      let y = (l + 16) / 116;
      let x = a / 500 + y;
      let z = y - b / 200;
  
      const y3 = Math.pow(y, 3);
      const x3 = Math.pow(x, 3);
      const z3 = Math.pow(z, 3);
  
      y = y3 > 0.008856 ? y3 : (y - 16 / 116) / 7.787;
      x = x3 > 0.008856 ? x3 : (x - 16 / 116) / 7.787;
      z = z3 > 0.008856 ? z3 : (z - 16 / 116) / 7.787;
  
      x *= 95.047;
      y *= 100.000;
      z *= 108.883;
  
      return [x, y, z];
    };
  
    const xyzToRgb = (x: number, y: number, z: number): [number, number, number] => {
      x /= 100;
      y /= 100;
      z /= 100;
  
      let r = x * 3.2406 + y * -1.5372 + z * -0.4986;
      let g = x * -0.9689 + y * 1.8758 + z * 0.0415;
      let b = x * 0.0557 + y * -0.2040 + z * 1.0570;
  
      r = r > 0.0031308 ? 1.055 * Math.pow(r, 1 / 2.4) - 0.055 : 12.92 * r;
      g = g > 0.0031308 ? 1.055 * Math.pow(g, 1 / 2.4) - 0.055 : 12.92 * g;
      b = b > 0.0031308 ? 1.055 * Math.pow(b, 1 / 2.4) - 0.055 : 12.92 * b;
  
      r = Math.min(Math.max(0, r), 1);
      g = Math.min(Math.max(0, g), 1);
      b = Math.min(Math.max(0, b), 1);
  
      return [Math.round(r * 255), Math.round(g * 255), Math.round(b * 255)];
    };
  
    const [x, y, z] = labToXyz(l, a, b);
    const rgb = xyzToRgb(x*10, y*10, z*10);
  
    if (rgb.some(value => value < 0 || value > 255)) {
      setErrorMessage("block")
      setErrorMessageText(`LAB(${l}, ${a}, ${b}) converted to out-of-gamut RGB(${rgb[0]}, ${rgb[1]}, ${rgb[2]})`)
      console.log(`LAB(${l}, ${a}, ${b}) converted to out-of-gamut RGB(${rgb[0]}, ${rgb[1]}, ${rgb[2]})`);
    }
    
    return rgb;
  };

  
  const hsvToRgb = (h: number, s: number, v: number): [number, number, number] => {
    setErrorMessage("none")

    s /= 100;
    v /= 100;
  
    const c = v * s; 
    const x = c * (1 - Math.abs((h / 60) % 2 - 1));
    const m = v - c;
  
    let r_prime: number, g_prime: number, b_prime: number;
  
    if (0 <= h && h < 60) {
      r_prime = c;
      g_prime = x;
      b_prime = 0;
    } else if (60 <= h && h < 120) {
      r_prime = x;
      g_prime = c;
      b_prime = 0;
    } else if (120 <= h && h < 180) {
      r_prime = 0;
      g_prime = c;
      b_prime = x;
    } else if (180 <= h && h < 240) {
      r_prime = 0;
      g_prime = x;
      b_prime = c;
    } else if (240 <= h && h < 300) {
      r_prime = x;
      g_prime = 0;
      b_prime = c;
    } else if (300 <= h && h < 360) {
      r_prime = c;
      g_prime = 0;
      b_prime = x;
    } else {
      r_prime = 0;
      g_prime = 0;
      b_prime = 0;
    }
  
    const r = Math.round((r_prime + m) * 255);
    const g = Math.round((g_prime + m) * 255);
    const b = Math.round((b_prime + m) * 255);
  
    if (r < 0 || r > 255 || g < 0 || g > 255 || b < 0 || b > 255) {
      setErrorMessage("block")
      setErrorMessageText(`HSV(${h}, ${s * 100}, ${v * 100}) converted to out-of-gamut RGB(${r}, ${g}, ${b})`)
      console.log(`HSV(${h}, ${s * 100}, ${v * 100}) converted to out-of-gamut RGB(${r}, ${g}, ${b})`);
    }
  
    return [r, g, b];
  };
  

  const rgbToHsv = (r: number, g: number, b: number): [number, number, number] => {
    
    r /= 255;
    g /= 255;
    b /= 255;
  
    const max = Math.max(r, g, b);
    const min = Math.min(r, g, b);
    const delta = max - min;
  
    let h: number, s: number, v: number = max;
  
    if (delta === 0) {
      h = 0;
    } else if (max === r) {
      h = ((g - b) / delta) % 6;
    } else if (max === g) {
      h = (b - r) / delta + 2;
    } else {
      h = (r - g) / delta + 4;
    }
  
    h = Math.round(h * 60);
    if (h < 0) h += 360;
  
    s = max === 0 ? 0 : delta / max;
    s = +(s * 100).toFixed(1);
    v = +(v * 100).toFixed(1);
  
    if (h < 0 || h > 360 || s < 0 || s > 100 || v < 0 || v > 100) {
      setErrorMessage("block")
      setErrorMessageText(`RGB(${r * 255}, ${g * 255}, ${b * 255}) converted to out-of-gamut HSV(${h}, ${s}, ${v})`)
      console.log(`RGB(${r * 255}, ${g * 255}, ${b * 255}) converted to out-of-gamut HSV(${h}, ${s}, ${v})`);
    }
  
    return [h, s, v];
  };


  const [rgb, setRgb] = useState<number[]>([255, 255, 255]);
  const [hsv, setHsv] = useState<number[]>(rgbToHsv(255, 255, 255));
  const [lab, setLab] = useState<number[]>([255, 255, 255]);

  const [rgbSliders, setRgbSliders] = useState<number[]>([255, 255, 255]);
  const [hsvsSliders, setHsvsSliders] = useState<number[]>(rgbToHsv(255, 255, 255));
  const [labSliders, setLabSliders] = useState<number[]>([255, 255, 255]);

  const RgbMaxValues = [255, 255, 255];
  const HsvMaxValues = [359, 100, 100];
  const LabMaxValues = [100, 127, 127];

  const RgbMinValues = [0, 0, 0];
  const HsvMinValues = [0, 0, 0];
  const LabMinValues = [0, -128, -128];

  const handleChange = (color: ColorResult) => {
    setColor(color.hex);
    setRgb([color.rgb.r, color.rgb.g, color.rgb.b])
    setLab(rgbToLab(rgb[0], rgb[1], rgb[2]))
    setHsv(rgbToHsv(color.rgb.r, color.rgb.g, color.rgb.b))

  };

  const handleRgbChange = (e: React.ChangeEvent<HTMLInputElement>, scheme: string, component: string, sliderCall: boolean = false) => {
    if (scheme == 'rgb') {
      const value = Math.max(0, Math.min(255, Number(e.target.value)));
      setRgbSliders(prev => {
        if (component == 'r') {
          prev[0] = value
        } else if (component == 'g') {
          prev[1] = value
        } else if (component == 'b') {
          prev[2] = value
        }
        return prev;
      })

      setRgb(prev => {
        const newRgb = prev;
        if (component == 'r') {
          newRgb[0] = value
        } else if (component == 'g') {
          newRgb[1] = value
        } else if (component == 'b') {
          newRgb[2] = value
        }

        setColor(`#${((1 << 24) + (newRgb[0] << 16) + (newRgb[1] << 8) + newRgb[2]).toString(16).slice(1)}`);
        return newRgb;
      });
      setHsv(rgbToHsv(rgb[0], rgb[1], rgb[2]))
      setLab(rgbToLab(rgb[0], rgb[1], rgb[2]))

    } else if (scheme == "hsv") {
      const value = Number(e.target.value)
      setHsv(prev => {
        const newHsv = [...prev];
        if (component == 'h') {
          newHsv[0] = value
        } else if (component == 's') {
          newHsv[1] = value
        } else if (component == 'v') {
          newHsv[2] = value
        }
        const newRgb = hsvToRgb(newHsv[0], newHsv[1], newHsv[2])
        setColor(`#${((1 << 24) + (newRgb[0] << 16) + (newRgb[1] << 8) + newRgb[2]).toString(16).slice(1)}`);
        console.log(component)
        console.log(newHsv)
        console.log(newRgb)
        return newHsv;
      });
      setRgb(hsvToRgb(hsv[0], hsv[1], hsv[2]))
      setLab(rgbToLab(rgb[0], rgb[1], rgb[2]))


    } else if (scheme == "lab") {
      const value = Number(e.target.value)
      setLab(prev => {
        const newLab = [...prev];
        if (component == 'l') {
          newLab[0] = value
        } else if (component == 'a') {
          newLab[1] = value
        } else if (component == 'b') {
          newLab[2] = value
        }
        const newRgb = labToRgb(newLab[0], newLab[1], newLab[2])
        setRgb(newRgb)
        setHsv(rgbToHsv(newRgb[0],newRgb[1],newRgb[2]))
        setColor(`#${((1 << 24) + (newRgb[0] << 16) + (newRgb[1] << 8) + newRgb[2]).toString(16).slice(1)}`);
        console.log(component)
        console.log(newLab)
        console.log(newRgb)
        return newLab;
      });
    }

    if (!sliderCall) {
      console.log("focus");
      e.target.focus();
    }
  }



  const handleSliderChange = (e: React.ChangeEvent<HTMLInputElement>, scheme: string, component: string) => {
    console.log("Slider change");
    // const event = { target: { value: e.target.value.toString() } } as React.ChangeEvent<HTMLInputElement>;
    handleRgbChange(e, scheme, component, true);
  };

  return (
    <Box>
      <Heading textAlign='center'>Laba 1, Hontarau Ivan, Variant 4</Heading>

      <Grid templateColumns='repeat(4, 2fr)' gap='20px' h='fit-content' padding='20px'
        alignContent='center' alignItems='center'
      >
        <GridItem height='100%' className='round-shadow' padding='5%' display='flex' justifyContent='center' backgroundColor={color}>
          <SketchPicker color={color} onChange={handleChange} width='90%' />
        </GridItem>
        <GridItem colSpan={3}
          alignContent='center' alignItems='center' height='100%'  >
          <Box display='grid' rowGap='auto' height='100%' >
            <ColorInput ColorScheme='rgb' rgb={rgb} maxValues={RgbMaxValues} minValues={RgbMinValues} sliderValues={rgbSliders}
              handleRgbChange={handleRgbChange} handleSliderChange={handleSliderChange} ></ColorInput>
            <ColorInput ColorScheme='hsv' rgb={hsv} maxValues={HsvMaxValues} minValues={HsvMinValues} sliderValues={hsvsSliders}
              handleRgbChange={handleRgbChange} handleSliderChange={handleSliderChange} ></ColorInput>
            <ColorInput ColorScheme='lab' rgb={lab} maxValues={LabMaxValues} minValues={LabMinValues} sliderValues={labSliders}
              handleRgbChange={handleRgbChange} handleSliderChange={handleSliderChange} ></ColorInput>
          </Box>

        </GridItem>
      </Grid>
      <Box width="100%">
        <Box  textAlign='center' backgroundColor="#ff3333" width="max-content"  display={errorMessage}
         color="white" margin="auto" padding="10px" marginTop="10px" rounded="50px" className='round-shadow' >
        Data is missing during convert: LAB
        </Box>
      </Box>
    </Box >

  );
}

export default App;
