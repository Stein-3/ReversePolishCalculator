import React from "react";
import { Card, ListGroup } from "react-bootstrap";
import { StackProps } from "./interface";
import "./Stack.css";

const Presenter: React.FC<StackProps> = (props) => {
  return (
    <Card className="stack-container">
      <Card.Header>Stackの状態</Card.Header>
      <ListGroup>{props.children}</ListGroup>
    </Card>
  );
};

export default Presenter;
