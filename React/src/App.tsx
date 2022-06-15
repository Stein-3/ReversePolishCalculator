import React from "react";
import { RecoilRoot } from "recoil";
import "./App.css";
import Content from "./Component/Content/Content";
import Header from "./Component/Header/Header";

const App: React.FC = () => {
  return (
    <div className="App">
      <RecoilRoot>
        <Header />
        <Content />
      </RecoilRoot>
    </div>
  );
};

export default App;
