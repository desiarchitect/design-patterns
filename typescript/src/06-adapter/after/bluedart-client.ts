export interface BluedartConsignmentRequest {
  refId: string;
  destination: string;
  weight: number;
}

export interface BluedartAwb {
  number: string;
}

export class BluedartClient {
  createConsignment(request: BluedartConsignmentRequest): BluedartAwb {
    console.log(`     [Bluedart SDK] CreateConsignment -> Ref:${request.refId}, Dest:${request.destination}`);
    return { number: `BD-${request.refId}-AWB` };
  }
}