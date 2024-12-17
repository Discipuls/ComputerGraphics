import { Box, GridItem, Input, InputGroup, InputLeftAddon, NumberInput, NumberInputField, RangeSlider, RangeSliderFilledTrack, RangeSliderThumb, RangeSliderTrack, Slide, Slider, SliderFilledTrack, SliderThumb, SliderTrack, VStack } from "@chakra-ui/react"
import { log } from "console";
import { createRef, ReactElement, useEffect, useRef, useState } from "react"

interface params {
    Parameter: string;
}

const NumberInputC = ({ Parameter, value, sliderValue, maxValue, minValue, handleChange, handleSliderChange }:
    {
        Parameter: string, value: number, sliderValue: number, maxValue: number, minValue: number, handleChange:
        (e: React.ChangeEvent<HTMLInputElement>) => void, handleSliderChange: (value: React.ChangeEvent<HTMLInputElement>) => void
    }): ReactElement => {
    return <GridItem>
        <InputGroup>
            <InputLeftAddon>{Parameter.toUpperCase()}</InputLeftAddon>
            <Input placeholder='100' type='number' value={value?.toString()} onChange={handleChange}  style={{ width: '70%' }}/>
        </InputGroup>
        <input min={minValue} max={maxValue} value={value} onChange={handleSliderChange} type="range" style={{ width: '100%' }} />
    </GridItem>
}

export default NumberInputC