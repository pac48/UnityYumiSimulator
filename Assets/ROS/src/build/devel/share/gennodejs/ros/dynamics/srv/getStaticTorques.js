// Auto-generated. Do not edit!

// (in-package dynamics.srv)


"use strict";

const _serializer = _ros_msg_utils.Serialize;
const _arraySerializer = _serializer.Array;
const _deserializer = _ros_msg_utils.Deserialize;
const _arrayDeserializer = _deserializer.Array;
const _finder = _ros_msg_utils.Find;
const _getByteLength = _ros_msg_utils.getByteLength;

//-----------------------------------------------------------


//-----------------------------------------------------------

class getStaticTorquesRequest {
  constructor(initObj={}) {
    if (initObj === null) {
      // initObj === null is a special case for deserialization where we don't initialize fields
      this.q = null;
    }
    else {
      if (initObj.hasOwnProperty('q')) {
        this.q = initObj.q
      }
      else {
        this.q = [];
      }
    }
  }

  static serialize(obj, buffer, bufferOffset) {
    // Serializes a message object of type getStaticTorquesRequest
    // Serialize message field [q]
    bufferOffset = _arraySerializer.float64(obj.q, buffer, bufferOffset, null);
    return bufferOffset;
  }

  static deserialize(buffer, bufferOffset=[0]) {
    //deserializes a message object of type getStaticTorquesRequest
    let len;
    let data = new getStaticTorquesRequest(null);
    // Deserialize message field [q]
    data.q = _arrayDeserializer.float64(buffer, bufferOffset, null)
    return data;
  }

  static getMessageSize(object) {
    let length = 0;
    length += 8 * object.q.length;
    return length + 4;
  }

  static datatype() {
    // Returns string type for a service object
    return 'dynamics/getStaticTorquesRequest';
  }

  static md5sum() {
    //Returns md5sum for a message object
    return 'ab94b9bcaaa12f74def43e4b33992df1';
  }

  static messageDefinition() {
    // Returns full string definition for message
    return `
    float64[] q
    
    `;
  }

  static Resolve(msg) {
    // deep-construct a valid message object instance of whatever was passed in
    if (typeof msg !== 'object' || msg === null) {
      msg = {};
    }
    const resolved = new getStaticTorquesRequest(null);
    if (msg.q !== undefined) {
      resolved.q = msg.q;
    }
    else {
      resolved.q = []
    }

    return resolved;
    }
};

class getStaticTorquesResponse {
  constructor(initObj={}) {
    if (initObj === null) {
      // initObj === null is a special case for deserialization where we don't initialize fields
      this.tau = null;
    }
    else {
      if (initObj.hasOwnProperty('tau')) {
        this.tau = initObj.tau
      }
      else {
        this.tau = [];
      }
    }
  }

  static serialize(obj, buffer, bufferOffset) {
    // Serializes a message object of type getStaticTorquesResponse
    // Serialize message field [tau]
    bufferOffset = _arraySerializer.float64(obj.tau, buffer, bufferOffset, null);
    return bufferOffset;
  }

  static deserialize(buffer, bufferOffset=[0]) {
    //deserializes a message object of type getStaticTorquesResponse
    let len;
    let data = new getStaticTorquesResponse(null);
    // Deserialize message field [tau]
    data.tau = _arrayDeserializer.float64(buffer, bufferOffset, null)
    return data;
  }

  static getMessageSize(object) {
    let length = 0;
    length += 8 * object.tau.length;
    return length + 4;
  }

  static datatype() {
    // Returns string type for a service object
    return 'dynamics/getStaticTorquesResponse';
  }

  static md5sum() {
    //Returns md5sum for a message object
    return '6488f72361a7c3710faf21208a8c781c';
  }

  static messageDefinition() {
    // Returns full string definition for message
    return `
    float64[] tau
    
    `;
  }

  static Resolve(msg) {
    // deep-construct a valid message object instance of whatever was passed in
    if (typeof msg !== 'object' || msg === null) {
      msg = {};
    }
    const resolved = new getStaticTorquesResponse(null);
    if (msg.tau !== undefined) {
      resolved.tau = msg.tau;
    }
    else {
      resolved.tau = []
    }

    return resolved;
    }
};

module.exports = {
  Request: getStaticTorquesRequest,
  Response: getStaticTorquesResponse,
  md5sum() { return '6b0ca527fdff0f3b38a61dfc2b4a926a'; },
  datatype() { return 'dynamics/getStaticTorques'; }
};
