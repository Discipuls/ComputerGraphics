import React, { ReactElement, useState } from 'react';
import logo from './logo.svg';
import { ColorResult, SketchPicker } from 'react-color';
import { Box, Grid, GridItem, Heading, NumberDecrementStepper, NumberIncrementStepper, NumberInput, NumberInputField, NumberInputStepper, Slider } from '@chakra-ui/react'
import NumberInputC from './input';

interface NumProps {
  ColorScheme: string;
}


const ColorInput = ({ ColorScheme, rgb, sliderValues, maxValues,minValues, handleRgbChange,handleSliderChange  }: 
  { ColorScheme: string, rgb: number[],sliderValues: number[], maxValues: number[],minValues: number[], handleRgbChange: 
  (e: React.ChangeEvent<HTMLInputElement>, scheme:string, component: string) => void ,
   handleSliderChange: (value: React.ChangeEvent<HTMLInputElement>, scheme:string, component: string) => void }): ReactElement  => {
  
  
  return (
    <Box padding='20px'  className='round-shadow' >
      {ColorScheme.toUpperCase()}
      <Grid templateColumns='repeat(3, 1fr)'  gap='10%'  marginTop='1%'>

        <NumberInputC Parameter={ColorScheme[0]} value={rgb[0]} maxValue={maxValues[0]} minValue={minValues[0]} sliderValue={sliderValues[0]}
         handleChange={(e) => handleRgbChange(e, ColorScheme, ColorScheme[0])}  
         handleSliderChange={(val) => handleSliderChange(val,ColorScheme, ColorScheme[0])} />
        <NumberInputC  Parameter={ColorScheme[1]} value={rgb[1]} maxValue={maxValues[1]} minValue={minValues[1]} sliderValue={sliderValues[1]}
        handleChange={(e) => handleRgbChange(e, ColorScheme, ColorScheme[1])}
          handleSliderChange={(val) => handleSliderChange(val, ColorScheme, ColorScheme[1])} />
        <NumberInputC  Parameter={ColorScheme[2]} value={rgb[2]} maxValue={maxValues[2]} minValue={minValues[2]} sliderValue={sliderValues[2]}
        handleChange={(e) => handleRgbChange(e, ColorScheme, ColorScheme[2])}  
        handleSliderChange={(val) => handleSliderChange(val, ColorScheme, ColorScheme[2])} />
      </Grid>
    </Box>

  );
}

export default ColorInput;
