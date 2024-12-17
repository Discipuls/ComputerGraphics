import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import { chakra, ChakraProvider, ColorModeScript, extendTheme, LightMode, theme, ThemeConfig } from '@chakra-ui/react';
import theme1 from './theme'
const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);

root.render(
  <React.StrictMode>
    <ColorModeScript initialColorMode={theme1.config.initialColorMode} />
    <ChakraProvider theme={theme1}>
      <App />
    </ChakraProvider>
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
